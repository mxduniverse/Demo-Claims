<script language="JScript" runat="server" src="/Scripts/json2.js"></script>

<% 

    If Request.ServerVariables("REQUEST_METHOD") = "POST" Then



dim url
url = "https://localhost:44398/api/jwt"
Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP")
HttpReq.open "POST", url, false
HttpReq.setRequestHeader "Content-Type", "application/json"
HttpReq.Send("{""Login"":"""& request.form("username") &""",""Password"":"""& request.form("password") &"""}")
'use json2 via jscript to parse the response



Set myJSON = JSON.parse(HttpReq.responseText)

    if myJSON.hasOwnProperty("userId") then

    Session("userId")=myJSON.userId
    Session("name")=myJSON.name
    Session("surname")=myJSON.surname
     Session("roleId")=myJSON.roleId
     Session("token")=myJSON.token
     Response.Redirect("index.asp")
    end if


End If

    %>

 <!--#include file="header2.asp"-->
<div class="col-md-6 offset-md-3 mt-5">

    <div class="card">
        <h4 class="card-header">Agent Web Application Login</h4>
        <div class="card-body">
            <form method="post" >
                <div class="form-group">
                    <label for="username">Username</label>
                    <input type="text" Name="username" class="form-control" required />
   
                </div>
                <div class="form-group">
                    <label for="password">Password</label>
                    <input type="password" name="password" required class="form-control"  />
     
                </div>
                <button  class="btn btn-primary">
                  
                    Login
                </button>
                <% if Request.ServerVariables("REQUEST_METHOD") = "POST" then
                    if myJSON.hasOwnProperty("message") then  %>
                <div  class="alert alert-danger mt-3 mb-0"> <% response.write myJSON.message  %> </div>
                <% end if
                    end if %>
            </form>
        </div>
    </div>
</div>



 <!--#include file="footer.asp"-->


