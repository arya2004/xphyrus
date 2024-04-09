import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Component, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { NexusService } from 'src/app/nexus/nexus.service';

import { Assignment, IAssignment } from 'src/app/shared/models/IAssesmentCreate';
import { AssessmentService } from '../assessment.service';
import { IAssessmentDetail } from 'src/app/shared/models/IAssessment';
import { ICodingAssessmentResult } from 'src/app/shared/models/IResults';
import { Subject } from 'rxjs';


declare var monaco: any;






@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  nexusId!: string;
  assessmentId!: string;

  id: string
  private sub: any;
  constructor(private fb:FormBuilder, private teacherService: NexusService, private ass:AssessmentService,private route: ActivatedRoute) {}

  lel: string = "1 2 3 4 5 <br>45 6";
  testCases: string[] = [
    "3 4 3 8<br>3 4 3 8",
    "1 5 6 5<br>1 5 6 5",
    "2 2 2 8<br>2 2 2 8",
    "5 3 5 7<br>5 3 5 7",
    "1 1 1 7<br>1 1 1 7",

  ]

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();


  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers'
    };
    this.sub = this.route.params.subscribe(params => {
      this.nexusId = params['id']; 
      this.assessmentId = params['codingAssessmentId']; // (+) converts string 'id' to a number
        
      });
    this.getAss()
    this.getRes()
    
  }


  longText = `The Shiba Inu is the smallest of the six original and distinct spitz breeds of dog
  from Japan. A small, agile dog that copes very well with mountainous terrain, the Shiba Inu was
  originally bred for hunting.`;

  description = "<p>Write a function to find the longest common prefix string amongst an array of strings.</p><p>If there is no common prefix, return an empty string&#160;<code>&#34;&#34;</code>.</p><p>&#160;</p><p><span class=\"example\">Example 1:</span></p><pre><span>Input:</span> strs = [&#34;flower&#34;,&#34;flow&#34;,&#34;flight&#34;]&#10;<span>Output:</span> &#34;fl&#34;&#10;</pre><p><span class=\"example\">Example 2:</span></p><pre><span>Input:</span> strs = [&#34;dog&#34;,&#34;racecar&#34;,&#34;car&#34;]&#10;<span>Output:</span> &#34;&#34;&#10;<span>Explanation:</span> There is no common prefix among the input strings.&#10;</pre><p>&#160;</p><p><span>Constraints:</span></p><ul><li><code>1 &lt;= strs.length &lt;= 200</code></li><li><code>0 &lt;= strs[i].length &lt;= 200</code></li><li><code>strs[i]</code>&#160;consists of only lowercase English letters.</li></ul>";
  


 

  assessment !: IAssessmentDetail  
  assResult : ICodingAssessmentResult[] = []

  getAss()
  {
    this.ass.getOneAssessment(this.assessmentId).subscribe({
      next: res => {
        console.log(res.result);
        
      this.assessment =  res.result;
      console.log(this.assessment)
      
    },
  
    error: err => console.log(err)
  });
  }

  getRes()
  {
    this.ass.getResults(this.assessmentId).subscribe({
      next: res => {
        console.log(res.result);
        
      this.assResult =  res.result;
      this.dtTrigger.next(null);
      console.log(this.assessment)
      
    },
  
    error: err => console.log(err)
  });
  }

 


}
