<script language="JScript" runat="server" src="/Scripts/json2.js"></script>
<%


          if not IsEmpty(request.querystring("did")) then

    url = "https://localhost:44398/api/claims/"&request.querystring("did")
Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP")
HttpReq.open "DELETE", url, false
HttpReq.setRequestHeader "Content-Type", "application/json"
HttpReq.setRequestHeader "Authorization", "Bearer " & session("token")
HttpReq.Send()


    end if
    
    dim url
url = "https://localhost:44398/api/claims"
Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP")
HttpReq.open "GET", url, false
HttpReq.setRequestHeader "Content-Type", "application/json"
HttpReq.setRequestHeader "Authorization", "Bearer " & session("token")
HttpReq.Send()


Set myJSON = JSON.parse(HttpReq.responseText)
    
'response.Write "response ="&  HttpReq.responseText




    
    %>

<!--#include file="header.asp"-->
<div class="container">
<div class="row">
    <div class="col-12">
        <div class="card">
            <div class="card-header">
                Claim Management System
                <a class="btn btn-success float-right" href="claimedit.asp">Add Claim</a>
            </div>
            <div class="card-body">
       <table class="table">
  <thead>
    <tr>
      <th scope="col">#</th> 
        <th scope="col">date</th>
      <th scope="col">UserId</th>
         <th scope="col">damagedItem</th>
         <th scope="col">incidence</th>
           <th scope="col">description</th>
  <th scope="col">address</th>
           <th scope="col">status</th>
    </tr>
  </thead>
  <tbody>
   <%   for each row in myJSON  %>
    <tr>
      <th scope="row"><% response.Write row.claimId %></th>
      <td><% response.Write row.date %></td>
      <td><% response.Write row.userId %></td>
      <td><% response.Write row.damagedItem %></td>
              <td><% response.Write row.incidence %></td>
      <td><% response.Write row.description %></td>
              <td><% response.Write row.address %></td>
              <td><% response.Write row.status %></td>

           <td><a href="claimedit.asp?id=<% response.Write row.claimId %>">Edit</a> &nbsp; &nbsp; <a href="claimmgmt.asp?did=<% response.Write row.claimId %>">Delete</a> </td>
    </tr>

   <% next %>   

  </tbody>
</table>
            </div>
        </div>

    </div>
</div>
</div>
<!--#include file="footer.asp"-->


