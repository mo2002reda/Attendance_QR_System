$(document).ready(function () {
    // Function to show images sequentially with fade effects
    function showImage(index) {
        // Fade in the current image
        if (index > 2) {
            // Redirect to the TheEnd action after #QRImage2
            window.location.href = '/Attendance/TheEnd'; // Adjust the URL to your action
            return;
        }
        $('#QRImage' + index).fadeIn(1500, function () {
            // Schedule the next image to show after 30 seconds
            setTimeout(function () {
                // Fade out the current image
                $('#QRImage' + index).fadeOut(1500, function () {
                    // Call the showImage function recursively with the next index
                    showImage(index + 1);
                });
            }, 30000); // 30 seconds
        });
    }

    // Hide all images initially except the first one
    $('#QRImage0, #QRImage1, #QRImage2, #QRImage3').hide();

    // Start the sequence by showing the first image
    showImage(0); // Adjusted to start from the first image
});




