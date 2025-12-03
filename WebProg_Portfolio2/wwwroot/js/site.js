// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function() {
    $("#countBtn").click(function() {
        $.get("/Image/GetCount").done(function(data) {
            if (data.success) $("#countResult").text("Antal billeder: " + data.count);
            else alert("Du skal være logget ind.");
        });
    });

    // Client-side regex validation for register form
    $("#registerForm").on("submit", function() {
        const email = $("#email").val();
        const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!re.test(email)) { alert("Ugyldig email"); return false; }
        return true;
    });
});