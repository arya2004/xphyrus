export interface AssesmentDto {
    Name: string | null | undefined;
     Description: string | null | undefined;
     Code: string | null | undefined;
     IsStrict: boolean;
     StartDate: Date;
     EndDate: Date;
     Duration: Date;
     Codings: CodingDto;
  }
  
  
  


export interface CodingDto {
     Title: string | null | undefined;
     Prompt: string | null | undefined;
     Language: string | null | undefined;
     InputFormat: string | null | undefined;
     OutputFormat: string | null | undefined;
     Constrain1: string | null | undefined;
     Constrain2: string | null | undefined;
     Constrain3: string | null | undefined;
     EvliationCases: EvliationCaseDto[] | null | undefined;
  }
  
  class EvliationCaseDto {
     InputCase: string | null | undefined;
     OutputCase: string | null | undefined;
  }

  
  