<%
dim username
dim loggedin
 username = Session("name")

loggedin = Session("userId")
If IsEmpty(username) Or IsEmpty(loggedin) Then
    Response.Redirect("login.asp")

End If

%>
