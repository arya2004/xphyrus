import { AfterViewInit, Component, OnInit, VERSION, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import {MatTableDataSource} from '@angular/material/table';

import { ActivatedRoute } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Assignment, IAssignment } from 'src/app/shared/models/IAssesmentCreate';
import { TeacherService } from '../teacher.service';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatSort, Sort } from '@angular/material/sort';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

const TEST_CASES: TestCase[] = [
  {position: 1, name: 'Hydrogen', weight: 1.0079},
  {position: 2, name: 'Helium', weight: 4.002},
  {position: 3, name: 'Lithium', weight: 6.941},
  {position: 4, name: 'Beryllium', weight: 9.0122},
  {position: 5, name: 'Boron', weight: 10.811},
  {position: 6, name: 'Carbon', weight: 12.0107},
  {position: 7, name: 'Nitrogen', weight: 14.0067},
  {position: 8, name: 'Oxygen', weight: 15.9994},
  {position: 9, name: 'Fluorine', weight: 18.9984},
  {position: 10, name: 'Neon', weight: 20.1797},
];


export interface TestCase {
  name: string;
  position: number;
  weight: number;
 
}

const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H'},
  {position: 2, name: 'Helium', weight: 4.0026, symbol: 'He'},
  {position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li'},
  {position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be'},
  {position: 5, name: 'Boron', weight: 10.811, symbol: 'B'},
  {position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C'},
  {position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N'},
  {position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O'},
  {position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F'},
  {position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne'},
];

@Component({
  selector: 'app-assesment',
  templateUrl: './assesment.component.html',
  styleUrls: ['./assesment.component.scss']
})



export class AssesmentComponent  implements AfterViewInit{
 
  constructor(private fb:FormBuilder, private teacherService: TeacherService,private _liveAnnouncer: LiveAnnouncer) {}

  ngOnInit(): void {
    this.addLesson();
  }

  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  longText = `The Shiba Inu is the smallest of the six original and distinct spitz breeds of dog
  from Japan. A small, agile dog that copes very well with mountainous terrain, the Shiba Inu was
  originally bred for hunting.`;

  description = "<font face=\"Arial\">sdf<u>sdf</u></font><p><font face=\"Arial\"><u>sdfsfd</u></font></p><p><font face=\"Arial\"><u><sup>jhjvhv</sup></u></font></p><h4><u><font face=\"Comic Sans MS\" size=\"3\"><sup>kjnkjnkj</sup></font></u></h4><h1><u><font face=\"Comic Sans MS\"><sup><font size=\"3\">kjbhvuvvvvvvvvvvv</font><font size=\"6\">jvvjv</font></sup></font></u></h1>";
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


  above = this. fb.group({
    title: ['']
  })
  form = this.fb.group({

    cases: this.fb.array([])
  });


  get cases() {
    return this.form.controls["cases"] as FormArray;
  }

  addLesson() {
    const lessonForm = this.fb.group({
      input: ['', Validators.required],
      output: ['', Validators.required]
    });
    this.cases.push(lessonForm);
  }



  onSubmit()
  { 
    const coding: IAssignment = new Assignment();
    coding.title = this.above.value.title
    coding.description = this.description
    coding.startDate = this.range.value.start
    coding.endDate = this.range.value.end
    coding.evaluationCases = this.cases.value
 
    console.log(JSON.stringify(coding));
    
      this.teacherService.postAssignment
  }

  displayedColumns: string[] = ['position', 'name', 'weight', 'symbol'];
  dataSource = new MatTableDataSource(ELEMENT_DATA);



  @ViewChild(MatSort) sort: MatSort;

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  announceSortChange(sortState: Sort) {
  
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }

  columns = [
    {
      columnDef: 'position',
      header: 'No.',
      cell: (element: TestCase) => `${element.position}`,
    },
    {
      columnDef: 'name',
      header: 'Name',
      cell: (element: TestCase) => `${element.name}`,
    },
    {
      columnDef: 'weight',
      header: 'Weight',
      cell: (element: TestCase) => `${element.weight}`,
    }
  ];
  dataSourceTestCase = TEST_CASES;
  displayedColumnsTestCase = this.columns.map(c => c.columnDef);
}
