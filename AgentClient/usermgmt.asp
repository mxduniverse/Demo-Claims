<script language="JScript" runat="server" src="/Scripts/json2.js"></script>
<%


          if not IsEmpty(request.querystring("did")) then

    url = "https://localhost:44362/api/UserInfo/"&request.querystring("did")
Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP")
HttpReq.open "DELETE", url, false
HttpReq.setRequestHeader "Content-Type", "application/json"
HttpReq.setRequestHeader "Authorization", "Bearer " & session("token")
HttpReq.Send()


    end if
    
    dim url
url = "https://localhost:44362/api/UserInfo/"
Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP")
HttpReq.open "GET", url, false
HttpReq.setRequestHeader "Content-Type", "application/json"
HttpReq.setRequestHeader "Authorization", "Bearer " & session("token")
HttpReq.Send()
'use json2 via jscript to parse the response

'response =[{"userId":1,"name":"landry","surname":"mukonde","login":"land","password":"password","roleId":1},{"userId":2,"name":"elisa","surname":"mukonde","login":"elisa","password":"password","roleId":2}]

Set myJSON = JSON.parse(HttpReq.responseText)
    
'response.Write "response ="&  HttpReq.responseText


    
    %>

<!--#include file="header.asp"-->
<div class="container">
<div class="row">
    <div class="col-12">
        <div class="card">
            <div class="card-header">
                Users Management System
                <a class="btn btn-success float-right" href="useredit.asp">Add User</a>
            </div>
            <div class="card-body">
       <table class="table">
  <thead>
    <tr>
      <th scope="col">#</th>
      <th scope="col">Name</th>
      <th scope="col">Surname</th>
         <th scope="col">Login</th>
         <th scope="col">Password</th>
           <th scope="col">Role</th>
           <th scope="col">Action</th>
    </tr>
  </thead>
  <tbody>
   <%   for each row in myJSON  %>
    <tr>
      <th scope="row"><% response.Write row.userId %></th>
      <td><% response.Write row.name %></td>
      <td><% response.Write row.surname %></td>
      <td><% response.Write row.login %></td>
              <td><% response.Write row.password %></td>
      <td><% response.Write row.roleId %></td>

           <td><a href="useredit.asp?id=<% response.Write row.userId %>">Edit</a> &nbsp; &nbsp; <a href="usermgmt.asp?did=<% response.Write row.userId %>">Delete</a> </td>
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


