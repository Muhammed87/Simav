<script>
    jQuery(document).ready(function() {
        jQuery('#showCheckoutHistory').change(function () {
            if ($(this).prop('checked')) {
                $('#showCheckoutHistory').Value = 1;
            }
            else {
                $('#showCheckoutHistory').Value = 0;
            }
        });
});
</script>