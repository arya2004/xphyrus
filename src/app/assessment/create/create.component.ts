import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { NexusService } from 'src/app/nexus/nexus.service';
import { Assignment, IAssignment } from 'src/app/shared/models/IAssesmentCreate';
import { AssessmentService } from '../assessment.service';


@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent {
  id!: string;
  private sub: any;

  constructor(private fb:FormBuilder, private teacherService: AssessmentService, private router: Router, private route: ActivatedRoute ) {}

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id']; // (+) converts string 'id' to a number
        console.log(this.id);
      });
      console.log(this.id);
  }


  description = "";
  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '15rem',
    minHeight: '5rem',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
   
  };


  selectedOption: string = "student";
  complexPasswd = "(?=^.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$"

  registerForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      associatedNexusId: [this.id]
    });

  onSubmit(){
    this.registerForm.value.startDate = new Date(this.registerForm.value.startDate).toISOString();
    this.registerForm.value.endDate = new Date(this.registerForm.value.endDate).toISOString();
    this.registerForm.value.associatedNexusId = this.id;
    console.log(this.registerForm.value);

    
    this.teacherService.postNexus(this.registerForm.value).subscribe({
      next: () => this.router.navigateByUrl('/Syndicate')
    })
  }

  
  
  

}
