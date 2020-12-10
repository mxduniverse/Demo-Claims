<!DOCTYPE html>
 <!--#include file="session.asp"-->
<html>
<head>
  <base href="/" />
  <title>Insurance Client Portal </title>
  <meta name="viewport" content="width=device-width, initial-scale=1">

  <!-- bootstrap css -->
  <link href="//netdna.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
  <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
  <script>
   
  </script>
</head>
<body>
  <!-- nav -->
  <nav class="navbar navbar-expand navbar-dark bg-dark" *ngIf="currentUser">
    <div class="navbar-nav">
      <a class="nav-item nav-link" href="/index.asp">Home</a>
      <a class="nav-item nav-link" (click)="logout()">Logout</a>
    </div>
  </nav>



  <!-- main app container -->
  <div class="container">
      <div class="row">
          <div class="col-12"></div>
        <h3>      Hi <% response.Write Session("name") %>  - <% response.Write  Session("surname") %> !</h3>


      </div>



 
 </div>
