import { Component, OnInit } from '@angular/core';
import { ClaimService } from 'src/app/services/claim.service';
import { ActivatedRoute, Router } from '@angular/router';

declare var $: any;

@Component({
  selector: 'app-claim-details',
  templateUrl: './claim-details.component.html',
  styleUrls: ['./claim-details.component.less']
})
export class ClaimDetailsComponent implements OnInit {

  currentClaim = null;
  message = '';

  constructor(
    private claimService: ClaimService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.message = '';
    this.getClaim(this.route.snapshot.paramMap.get('id'));
    $(function () {
      var availableTags = [
        "House", "car", "laptop", "phone", "printer", "Bike", "MTB Bike", "Shoes"
      ];

      var IncidenceTags = [
        "Food", "tornado", "thunder", "flood", "theft"
      ];
      $("#damagedItem").autocomplete({
        source: availableTags
      });
      $("#Incidence").autocomplete({
        source: IncidenceTags
      });

    });
  }

  getClaim(id) {
    this.claimService.get(id)
      .subscribe(
        data => {
          this.currentClaim = data;
          console.log(data);
        },
        error => {
          console.log(error);
        });
  }


  updateClaim() {
    this.claimService.update(this.currentClaim.claimId, this.currentClaim)
      .subscribe(
        response => {
          console.log(response);
          this.message = 'The claim was updated successfully!';
        },
        error => {
          console.log(error);
        });
  }

  deleteClaim() {
    this.claimService.delete(this.currentClaim.claimId)
      .subscribe(
        response => {
          console.log(response);
          this.router.navigate(['/claims']);
        },
        error => {
          console.log(error);
        });
  }

}
