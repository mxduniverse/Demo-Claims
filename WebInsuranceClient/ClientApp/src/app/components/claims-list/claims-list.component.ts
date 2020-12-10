import { Component, OnInit } from '@angular/core';
import { ClaimService } from 'src/app/services/claim.service';
import { User } from '@app/_models';
import { AuthenticationService } from '@app/_services';



@Component({
  selector: 'app-claims-list',
  templateUrl: './claims-list.component.html',
  styleUrls: ['./claims-list.component.less']
})
export class ClaimsListComponent implements OnInit {


  claims: any;
  user: User;
  currentClaim = null;
  currentIndex = -1;
  Description = '';



  constructor(private ClaimService: ClaimService, private accountService: AuthenticationService) {
    this.user = accountService.currentUserValue;
   
  }

  ngOnInit() {
    this.retrieveClaims();




  }


  


  retrieveClaims() {
    this.ClaimService.getAll()
      .subscribe(
        data => {
          this.claims = data;
          console.log(data);
        },
        error => {
          console.log(error);
        });
  }

  refreshList() {
    this.retrieveClaims();
    this.currentClaim = null;
    this.currentIndex = -1;
  }

  setActiveclaim(claim, index) {
    this.currentClaim = claim;
    this.currentIndex = index;
  }

  removeAllClaims() {
    this.ClaimService.deleteAll()
      .subscribe(
        response => {
          console.log(response);
          this.retrieveClaims();
        },
        error => {
          console.log(error);
        });
  }

  searchTitle() {
    this.ClaimService.findByTitle(this.Description)
      .subscribe(
        data => {
          this.claims = data;
          console.log(data);
        },
        error => {
          console.log(error);
        });
  }
}

