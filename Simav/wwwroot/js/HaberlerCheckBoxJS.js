jQuery(document).ready(function() {
        jQuery('#cbOnayla').change(function () {
            if ($(this).prop('checked')) {
                $('#hdOnayla').val('1');
            }
            else {
                $('#hdOnayla').val('0');
            }
        });
});
