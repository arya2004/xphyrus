using EvaluationService.Dtos;
using EvaluationService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace EvaluationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeExecutionController : ControllerBase
    {
        [HttpPost("execute")]
        public async Task<IActionResult> ExecuteCode([FromBody] CodingAssessmentSubmission submission)
        {
            try
            {
                var result = await ExecuteSourceCode(submission);
                return Ok(result);
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        private async Task<SubmissionStatusResponse> ExecuteSourceCode(CodingAssessmentSubmission msg)
        {
            string fileName = $"temp_{Guid.NewGuid()}";
            string filePath = Path.Combine(Path.GetTempPath(), fileName);
            string compiledFilePath = string.Empty;
            string compiler = string.Empty;
            string arguments = string.Empty;
            bool needsCompilation = false;

            switch (msg.Language)
            {
                case "50": // C
                    filePath += ".c";
                    compiledFilePath = filePath.Replace(".c", ".out");
                    compiler = "gcc";
                    arguments = $"{filePath} -o {compiledFilePath}";
                    needsCompilation = true;
                    break;
                case "54": // C++
                    filePath += ".cpp";
                    compiledFilePath = filePath.Replace(".cpp", ".out");
                    compiler = "g++";
                    arguments = $"{filePath} -o {compiledFilePath}";
                    needsCompilation = true;
                    break;
                case "51": // C#
                    filePath += ".cs";
                    compiledFilePath = filePath.Replace(".cs", ".exe");
                    compiler = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe";  // Use the full path to csc.exe
                    arguments = $"/out:{compiledFilePath} {filePath}";
                    needsCompilation = true;
                    break;
                case "62": // Java
                    var className = "Main"; // Default to Main; you can enhance this by parsing the class name from the code
                    if (msg.Source_code.Contains("public class "))
                    {
                        int startIndex = msg.Source_code.IndexOf("public class ") + "public class ".Length;
                        int endIndex = msg.Source_code.IndexOf(" ", startIndex);
                        className = msg.Source_code.Substring(startIndex, endIndex - startIndex).Trim();
                    }
                    filePath = Path.Combine(Path.GetTempPath(), className + ".java");
                    compiledFilePath = Path.Combine(Path.GetTempPath(), className);
                    compiler = "javac";
                    arguments = filePath;
                    needsCompilation = true;
                    break;
                case "71": // Python
                    filePath += ".py";
                    compiledFilePath = filePath;
                    compiler = "python";
                    arguments = filePath;
                    needsCompilation = false;
                    break;
                default:
                    throw new NotSupportedException("Language not supported");
            }

            // Write the source code to the file
            await System.IO.File.WriteAllTextAsync(filePath, msg.Source_code);

            // Compile the code if needed
            if (needsCompilation)
            {
                var compileProcess = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = compiler,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                compileProcess.Start();
                string compileError = await compileProcess.StandardError.ReadToEndAsync();
                compileProcess.WaitForExit();

                if (compileProcess.ExitCode != 0)
                {
                    // Clean up the temporary source file
                    System.IO.File.Delete(filePath);

                    return new SubmissionStatusResponse
                    {
                        stdout = "",
                        stderr = compileError,
                        status = new Status { description = "Compilation Error" }
                    };
                }
            }

            // Determine the execution command
            string executionCommand = msg.Language switch
            {
                "50" or "54" => compiledFilePath,
                "51" => compiledFilePath,
                "62" => "java",
                "71" => compiler,
                _ => throw new NotSupportedException("Language not supported")
            };

            // Adjust arguments for Java execution
            if (msg.Language == "62")
            {
                arguments = $"-cp {Path.GetTempPath()} {Path.GetFileNameWithoutExtension(filePath)}";
            }

            // Execute the compiled code or script
            var executeProcess = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = executionCommand,
                    Arguments = msg.Language == "71" ? arguments : arguments,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            executeProcess.Start();

            if (!string.IsNullOrEmpty(msg.Input))
            {
                await executeProcess.StandardInput.WriteLineAsync(msg.Input);
                await executeProcess.StandardInput.FlushAsync();
                executeProcess.StandardInput.Close();
            }

            string output = await executeProcess.StandardOutput.ReadToEndAsync();
            string error = await executeProcess.StandardError.ReadToEndAsync();
            executeProcess.WaitForExit();

            // Clean up the temporary files
            System.IO.File.Delete(filePath);
            if (needsCompilation && msg.Language != "62") // Java compiled files are multiple; they will be handled differently
            {
                System.IO.File.Delete(compiledFilePath);
            }

            return new SubmissionStatusResponse
            {
                stdout = output,
                stderr = error,
                status = new Status { description = executeProcess.ExitCode == 0 ? "Success" : "Error" }
            };
        }


    }
}