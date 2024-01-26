import { AfterViewInit, Component, OnInit, VERSION, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import {MatTableDataSource} from '@angular/material/table';

import { ActivatedRoute } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Assignment, IAssignment } from 'src/app/shared/models/IAssesmentCreate';

import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatSort, Sort } from '@angular/material/sort';

import { ClassroomService } from 'src/app/classroom/classroom.service';




declare var monaco: any;


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
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent{
  
  id: string;
  private sub: any;
  constructor(private fb:FormBuilder, private teacherService: ClassroomService,private _liveAnnouncer: LiveAnnouncer) {}

  lel: string = "1 2 3 4 5 <br>45 6";
  testCases: string[] = [
    "3 4 3 8<br>3 4 3 8",
    "1 5 6 5<br>1 5 6 5",
    "2 2 2 8<br>2 2 2 8",
    "5 3 5 7<br>5 3 5 7",
    "1 1 1 7<br>1 1 1 7",

  ]

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

  description = "<p>Write a function to find the longest common prefix string amongst an array of strings.</p><p>If there is no common prefix, return an empty string&#160;<code>&#34;&#34;</code>.</p><p>&#160;</p><p><span class=\"example\">Example 1:</span></p><pre><span>Input:</span> strs = [&#34;flower&#34;,&#34;flow&#34;,&#34;flight&#34;]&#10;<span>Output:</span> &#34;fl&#34;&#10;</pre><p><span class=\"example\">Example 2:</span></p><pre><span>Input:</span> strs = [&#34;dog&#34;,&#34;racecar&#34;,&#34;car&#34;]&#10;<span>Output:</span> &#34;&#34;&#10;<span>Explanation:</span> There is no common prefix among the input strings.&#10;</pre><p>&#160;</p><p><span>Constraints:</span></p><ul><li><code>1 &lt;= strs.length &lt;= 200</code></li><li><code>0 &lt;= strs[i].length &lt;= 200</code></li><li><code>strs[i]</code>&#160;consists of only lowercase English letters.</li></ul>";
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
