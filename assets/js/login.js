var login_button = document.getElementById("login_button").addEventListener("click", function(){
    let username = document.getElementById("username").value;
    let password = document.getElementById("password").value;
    console.log(username , password);
    let formlogin = document.getElementById("login__form").style.display = "none";
});