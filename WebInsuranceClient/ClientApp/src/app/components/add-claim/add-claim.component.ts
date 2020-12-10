import { Component, OnInit } from '@angular/core';
import { ClaimService } from 'src/app/services/claim.service';
import { User } from '@app/_models';
import { AuthenticationService } from '@app/_services';

declare var $: any;

@Component({
  selector: 'app-add-claim',
  templateUrl: './add-claim.component.html',
  styleUrls: ['./add-claim.component.less']
})
export class AddClaimComponent implements OnInit {

  user: User;
  claim =
    {
        ClaimId:0,
      UserId: 0,
        Date:'',
        Address:'',
        DamagedItem:'',
       Description:'',
        Status:'New',
        Incidence:''
    }
  submitted = false;


  constructor(private ClaimService: ClaimService, private accountService: AuthenticationService) {
    this.user = accountService.currentUserValue;

  }

  ngOnInit() {

    $(function () {
      var availableTags = [
        "House", "car", "laptop", "phone", "printer", "Bike", "MTB Bike", "Shoes"
      ];

      var IncidenceTags = [
        "Food", "tornado", "thunder","flood", "theft"
      ];
      $("#damagedItem").autocomplete({
        source: availableTags
      });
      $("#Incidence").autocomplete({
        source: IncidenceTags
      });

    });
  }

 


  saveClaim() {
    var userid: number = this.user.userId;
    const data = {
      ClaimId: 0,
      UserId:Number(userid),
      Date: this.claim.Date,
      Address: this.claim.Address,
      DamagedItem: this.claim.DamagedItem,
      Description: this.claim.Description,
      Status: "New",
      Incidence: this.claim.Incidence
    };

    this.ClaimService.create(data)
      .subscribe(
        response => {
          console.log(response);
          this.submitted = true;
        },
        error => {
          console.log(error);
        });
  }

  newClaim() {
    this.submitted = false;
    this.claim = {
      ClaimId: 0,
      UserId: 0,
      Date: '',
      Address: '',
      DamagedItem: '',
      Description: '',
      Status: 'New',
      Incidence: ''
    };
  }

}
