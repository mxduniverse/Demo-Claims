<script language="JScript" runat="server" src="/Scripts/json2.js"></script>
<%


    
    dim url
url = "https://localhost:44306/api/logs"
Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP")
HttpReq.open "GET", url, false
HttpReq.setRequestHeader "Content-Type", "application/json"
HttpReq.setRequestHeader "Authorization", "Bearer " & session("token")
HttpReq.Send()
'use json2 via jscript to parse the response


Set myJSON = JSON.parse(HttpReq.responseText)
    



    
    %>

<!--#include file="header.asp"-->
<div class="container">
<div class="row">
    <div class="col-12">
        <div class="card">
            <div class="card-header">
                Logs Management System

            </div>
            <div class="card-body">
       <table class="table">
  <thead>
    <tr>
      <th scope="col">#</th>
      <th scope="col">Time Stamp</th>
      <th scope="col">User Id</th>
         <th scope="col">Username</th>
         <th scope="col">Action Performed</th>

    </tr>
  </thead>

  <tbody>
   <%   for each row in myJSON  %>
    <tr>
      <th scope="row"><% response.Write row.logId %></th>
      <td><% response.Write row.timeStamp %></td>
              <td><% response.Write row.userId %></td>
      <td><% response.Write row.userName %></td>
      <td><% response.Write row.actionPerformed %></td>

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


