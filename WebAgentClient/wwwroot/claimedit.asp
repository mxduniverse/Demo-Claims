<script language="JScript" runat="server" src="/Scripts/json2.js"></script>
<%

     dateclaim = ""
     damagedItem = ""
     incidence =""
     description =""
    address = ""
    status =status
    typetxt ="Add"
    userId=0



  if not IsEmpty(request.querystring("id")) then
    typetxt ="Edit"
    url = "https://localhost:44398/api/claims/"&request.querystring("id")
Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP")
HttpReq.open "GET", url, false
HttpReq.setRequestHeader "Content-Type", "application/json"
HttpReq.setRequestHeader "Authorization", "Bearer " & session("token")
HttpReq.Send()
    Set myJSON = JSON.parse(HttpReq.responseText)
   'response.write HttpReq.responseText 
     dateclaim =replace( myJSON.date,"T00:00:00","")
     damagedItem = myJSON.damagedItem
     incidence =myJSON.incidence
     description =myJSON.description
    address = myJSON.address
    status =myJSON.status
    typetxt ="Add"
    userId=myJSON.userId


    end if




  
        If Request.ServerVariables("REQUEST_METHOD") = "POST" Then
    dim url
     if  IsEmpty(request.querystring("id")) then
url = "https://localhost:44398/api/claims"
Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP")
            HttpReq.open "POST", url, false
HttpReq.setRequestHeader "Content-Type", "application/json"
HttpReq.setRequestHeader "Authorization", "Bearer " & session("token")
HttpReq.Send("{""claimId"":0,""userId"":"& request.form("userID") &",""date"":"""& request.form("date") &""",""address"":"""& request.form("address") &""",""damagedItem"":"""& request.form("damagedItem") &""",""description"":"""& request.form("description") &""",""status"":"""& request.form("status") &""",""incidence"":"""& request.form("incidence") &"""}")
 else
url = "https://localhost:44398/api/claims/"&request.querystring("id")
Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP")
            HttpReq.open "PUT", url, false
HttpReq.setRequestHeader "Content-Type", "application/json"
HttpReq.setRequestHeader "Authorization", "Bearer " & session("token")
    HttpReq.Send("{""claimId"":"&request.querystring("id")&",""userId"":"& request.form("userID") &",""date"":"""& request.form("date") &""",""address"":"""& request.form("address") &""",""damagedItem"":"""& request.form("damagedItem") &""",""description"":"""& request.form("description") &""",""status"":"""& request.form("status") &""",""incidence"":"""& request.form("incidence") &"""}")

     Response.Redirect("claimmgmt.asp")

    end if
 
Set myJSON = JSON.parse(HttpReq.responseText)
 if myJSON.hasOwnProperty("claimId") then
     Response.Redirect("claimmgmt.asp")
    end if
    


end if

url = "https://localhost:44362/api/UserInfo/"
Set HttpReq2 = Server.CreateObject("MSXML2.ServerXMLHTTP")
HttpReq2.open "GET", url, false
HttpReq2.setRequestHeader "Content-Type", "application/json"
HttpReq2.setRequestHeader "Authorization", "Bearer " & session("token")
HttpReq2.Send()
Set myJSON2 = JSON.parse(HttpReq2.responseText)


    
    %>

<!--#include file="header.asp"-->
<div class="container">
<div class="row">
    <div class="col-12">
        <div class="card">
            <div class="card-header">
                Users Management System -  <%response.Write typetxt %> Claim
            </div>
            <div class="card-body">
               <form method="post" >

                                   <div class="form-group">
                    <label for="date">Date</label>
                    <input type="date" Name="date" value="<% response.write dateclaim %>" class="form-control" required />
   
                </div>
                <div class="form-group">
                    <label for="surname">Names</label>
                        <select name="userId" class="form-control" id="userId">
                            <%  for each row in myJSON2  %>
                                  <option value="<% response.write row.userID %>" <% if row.userID=userId then response.write "selected" end if %> ><% response.write row.name &" "& row.surname %></option>
                            <% next %>

</select>
     
                </div>

                <div class="form-group">
                    <label for="name">Damaged Item</label>
                    <input type="text" Name="damagedItem"  id="damagedItem" value="<% response.write damagedItem %>" class="form-control" required />
   
                </div>
                <div class="form-group">
                    <label for="surname">Incidence</label>
                    <input type="text"  id="incidence"  name="incidence" value="<% response.write incidence %>" required class="form-control"  />
     
                </div>

                <div class="form-group">
                    <label for="username">Description</label>
                    <input type="text" Name="description" value="<% response.write description %>" class="form-control" required />
   
                </div>
          <div class="form-group">
                    <label for="address">Adress</label>
                    <input type="text" Name="address" value="<% response.write address %>" class="form-control" required />
   
                </div>
                    <div class="form-group">
    <label for="role">Status</label>
    <select name="status" class="form-control" id="role">
      <option value="New" <% if role="New" then response.write "selected" end if %> >New</option>
      <option value="" <% if role="Review" then response.write "selected" end if %>>Review</option>
            <option value="Pending" <% if role="Pending" then response.write "selected" end if %> >Pending</option>
      <option value="Approved" <% if role="Approved" then response.write "selected" end if %>>Approved</option>
           <option value="Closed" <% if role="Closed" then response.write "selected" end if %>>Closed</option>
    </select>
  </div>


                <button  class="btn btn-primary">
                        Save
                </button>

                       <a class="btn btn-success float-right" href="claimmgmt.asp">Back</a>
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


