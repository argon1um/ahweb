﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('click', function(event) {
    if (event.target.matches('.card')) {
        BookingPageModel.selectedRoomid = (event.target.id);
    }
});