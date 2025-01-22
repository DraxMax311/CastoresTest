$(document).ready(function () {
    // Check if the current route is "NewOrder"
    var currentUrl = window.location.pathname.toLowerCase();
    var isNewOrderRoute = currentUrl.includes("/neworder");

    // Only enable sidebar if on the NewOrder route
    if (isNewOrderRoute) {
        // Toggle sidebar visibility
        $('#sidebarToggle').on('click', function (e) {
            e.stopPropagation(); // Prevent the click from propagating to the document
            $('#sidebar').toggleClass('active');
            $('#content').toggleClass('shifted');
        });
        $('#sidebarClose').on('click', function (e) {
            e.stopPropagation(); // Prevent the click from propagating to the document
            $('#sidebar').toggleClass('active');
            $('#content').toggleClass('shifted');
        });
    }
    else {
        // If not on the NewOrder route, hide sidebar toggle and sidebar by default
        $('#sidebar').removeClass('active');
        $('#content').removeClass('shifted');
    }
});