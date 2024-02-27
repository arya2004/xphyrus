import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {
  id: string;
  private sub: any;
  constructor(private route: ActivatedRoute) { }
  




  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
    this.id = params['id']; // (+) converts string 'id' to a number
      console.log(this.id);
    });
  }



  syndicateId: number = 123; // Replace with your dynamic value
assignmentId: number = 456; // Replace with your dynamic value
syndicatePath: string = `/syndicate/${this.syndicateId}/assignment/${this.assignmentId}`;

}
