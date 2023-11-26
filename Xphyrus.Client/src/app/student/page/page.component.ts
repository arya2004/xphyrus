import { Component, OnInit } from '@angular/core';
import { DiffEditorModel, NgxEditorModel } from 'ngx-monaco-editor-v2';
import { StudentService } from '../student.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { IAssesmentHome } from 'src/app/shared/models/IAssesmentAdminHome';

declare var monaco: any;

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.scss']
})
export class PageComponent  {
  myJoinedAssingments: IAssesmentHome[] = []
  constructor(private fb:FormBuilder, private studentService: StudentService,private router: Router) {}

  codeInput = 'Sample Code';
  editor: any;
  diffEditor: any;
  showMultiple = false;
  toggleLanguage = true;
  options = {
    theme: 'vs-dark'
  };
  code: string;
  cssCode = `.my-class {
  color: red;
}`;
  jsCode = `function hello() {
	 alert('Hello world!');
}`;

  originalModel: DiffEditorModel = {
    code: 'heLLo world!',
    language: 'text/plain'
  };

  modifiedModel: DiffEditorModel = {
    code: 'hello orlando!',
    language: 'text/plain'
  };

  jsonCode = [
    '{',
    '    "p1": "v3",',
    '    "p2": false',
    '}'
  ].join('\n');

  model: NgxEditorModel = {
    value: this.jsonCode,
    language: 'json'
  };

  ngOnInit() {
    this.updateOptions();
    this.viewDashboard()
  }

  updateOptions() {
    this.toggleLanguage = !this.toggleLanguage;
    if (this.toggleLanguage) {
      this.code = this.cssCode;
      this.options = Object.assign({}, this.options, { language: 'c' });
    } else {
      this.code = this.jsCode;
      this.options = Object.assign({}, this.options, { language: 'javascript' });
    }

  }

 

  onInit(editor : any) {
    this.editor = editor;
    console.log(editor);
    this.model = {
      value: this.jsonCode,
      language: 'json',
      uri: monaco.Uri.parse('a://b/foo.json')
    };
    // let line = editor.getPosition();
    // let range = new monaco.Range(line.lineNumber, 1, line.lineNumber, 1);
    // let id = { major: 1, minor: 1 };
    // let text = 'FOO';
    // let op = { identifier: id, range: range, text: text, forceMoveMarkers: true };
    // editor.executeEdits("my-source", [op]);

   
  }
  viewDashboard(){
    this.studentService.getJoined().subscribe({
      next: res => {
        this.myJoinedAssingments  = res.result
        console.log(this.myJoinedAssingments);
        
      }
    })
  }

  codeId = this. fb.group({
    code: ['']
  })
  onSubmit()
  {
    console.log(this.codeId.value.code);
    
   this.studentService.joinAssemsnet(this.codeId.value.code).subscribe({
      next: () => {
        window.location.reload()
      },
      error: () => this.router.navigateByUrl('/')
   })
    
  }


 

}
