<script language="JScript" runat="server" src="/Scripts/json2.js"></script>
<%

     name = ""
     surname = ""
     username =""
     password =""
    role = 0
    typetxt ="Add"
  if not IsEmpty(request.querystring("id")) then
    typetxt ="Edit"
    url = "https://localhost:44362/api/UserInfo/"&request.querystring("id")
Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP")
HttpReq.open "GET", url, false
HttpReq.setRequestHeader "Content-Type", "application/json"
HttpReq.setRequestHeader "Authorization", "Bearer " & session("token")
HttpReq.Send()
    Set myJSON = JSON.parse(HttpReq.responseText)
      name = myJSON.name
     surname = myJSON.surname
     username =myJSON.login
     password =myJSON.password
    role = myJSON.roleId

    end if




  
        If Request.ServerVariables("REQUEST_METHOD") = "POST" Then
    dim url
     if  IsEmpty(request.querystring("id")) then
url = "https://localhost:44362/api/UserInfo"
Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP")
            HttpReq.open "POST", url, false
HttpReq.setRequestHeader "Content-Type", "application/json"
HttpReq.setRequestHeader "Authorization", "Bearer " & session("token")
HttpReq.Send("{""userId"":0,""name"":"""& request.form("name") &""",""surname"":"""& request.form("surname") &""",""login"":"""& request.form("username") &""",""password"":"""& request.form("password") &""",""roleId"":"& request.form("roleId") &"}")
 else
url = "https://localhost:44362/api/UserInfo/"&request.querystring("id")
Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP")
            HttpReq.open "PUT", url, false
HttpReq.setRequestHeader "Content-Type", "application/json"
HttpReq.setRequestHeader "Authorization", "Bearer " & session("token")
HttpReq.Send("{""userId"":"&request.querystring("id")&",""name"":"""& request.form("name") &""",""surname"":"""& request.form("surname") &""",""login"":"""& request.form("username") &""",""password"":"""& request.form("password") &""",""roleId"":"& request.form("roleId") &"}")


    end if

  Response.Write  "response:"&HttpReq.responseText
Set myJSON = JSON.parse(HttpReq.responseText)
 if myJSON.hasOwnProperty("userId") then
     Response.Redirect("usermgmt.asp")
    end if
    


end if
    
    %>

<!--#include file="header.asp"-->
<div class="container">
<div class="row">
    <div class="col-12">
        <div class="card">
            <div class="card-header">
                Users Management System -  <%response.Write typetxt %> User
            </div>
            <div class="card-body">
               <form method="post" >

                <div class="form-group">
                    <label for="name">Name</label>
                    <input type="text" Name="name" value="<% response.write name %>" class="form-control" required />
   
                </div>
                <div class="form-group">
                    <label for="surname">Surname</label>
                    <input type="text" name="surname" value="<% response.write surname %>" required class="form-control"  />
     
                </div>

                <div class="form-group">
                    <label for="username">Username</label>
                    <input type="text" Name="username" value="<% response.write username %>" class="form-control" required />
   
                </div>
                <div class="form-group">
                    <label for="password">Password</label>
                    <input type="password" name="password" value="<% response.write password %>" required class="form-control"  />
     
                </div>
                    <div class="form-group">
    <label for="role">Role</label>
    <select name="roleId" class="form-control" id="role">
      <option value="1" <% if role=1 then response.write "selected" end if %> >Agent</option>
      <option value="2" <% if role=2 then response.write "selected" end if %>>Client</option>
    </select>
  </div>


                <button  class="btn btn-primary">
                        Save
                </button>

                       <a class="btn btn-success float-right" href="usermgmt.asp">Back</a>
                <% if Request.ServerVariables("REQUEST_METHOD") = "POST" then
                    if myJSON.hasOwnProperty("message") then  %>
                <div  class="alert alert-danger mt-3 mb-0"> <% response.write myJSON.message  %> </div>
                <% end if
                    end if %>
            </form>
            </div>
        </div>

    </div>
</div>
</div>
<!--#include file="footer.asp"-->


