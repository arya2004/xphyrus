import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-newassesment',
  templateUrl: './newassesment.component.html',
  styleUrls: ['./newassesment.component.scss']
})
export class NewassesmentComponent {
  assessmentForm: FormGroup;

  constructor(public formBuilder: FormBuilder) {}

  ngOnInit() {
    this.initForm();
  }
  

  initForm() {
    this.assessmentForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      code: ['', Validators.required],
      isStrict: [false],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      duration: ['', Validators.required],
      codings: this.formBuilder.array([]),
      mcQs: this.formBuilder.array([])
    });
  }

  get codingsFormArray() {
    return this.assessmentForm.get('codings') as FormArray;
  }

  get mcQsFormArray() {
    return this.assessmentForm.get('mcQs') as FormArray;
  }

  addCoding() {
    const codingGroup = this.formBuilder.group({
      title: ['', Validators.required],
      prompt: ['', Validators.required],
      language: ['', Validators.required],
      inputFormat: ['', Validators.required],
      outputFormat: ['', Validators.required],
      constrain1: [''],
      constrain2: [''],
      constrain3: [''],
      evaluationCases: this.formBuilder.array([])
    });

    this.codingsFormArray.push(codingGroup);
  }

  removeCoding(index: number) {
    this.codingsFormArray.removeAt(index);
  }

  addMcQ() {
    const mcqGroup = this.formBuilder.group({
      question: ['', Validators.required],
      correctAnswer: [0, Validators.required],
      options: this.formBuilder.array([
        this.createOptionGroup(),
        this.createOptionGroup()
      ])
    });

    this.mcQsFormArray.push(mcqGroup);
  }

  removeMcQ(index: number) {
    this.mcQsFormArray.removeAt(index);
  }

  createOptionGroup() {
    return this.formBuilder.group({
      optionNumber: [0, Validators.required],
      value: ['', Validators.required]
    });
  }

  addEvaluationCase(evaluationCasesFormArray: any) {
    evaluationCasesFormArray.push(this.formBuilder.group({
      inputCase: [''],
      outputCase: ['']
    }));
  }

  removeEvaluationCase(evaluationCasesFormArray: any, index: number) {
    evaluationCasesFormArray.removeAt(index);
  }
  addOption(optionsFormArray: any) {
    optionsFormArray.push(this.createOptionGroup());
  }

  removeOption(optionsFormArray: any, index: number) {
    optionsFormArray.removeAt(index);
  }

  submut()
  {
    console.log(this.assessmentForm.value);
    
  }
  trackByFn(index: any, item: any) {
    return index;
 }
}