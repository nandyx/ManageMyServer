$(document).ready(function() {
	$('#output').hide();
	//$('#database').hide();
    $('[id^=item-]').hide();
    $('.toggle').click(function() {
        $input = $( this );
        $target = $('#'+$input.attr('data-toggle'));
        $target.slideToggle();
    });
    //$('#launch').click(function() {
    //	$('#output').toggle('slow',function() {
    //		$('#database').show('fast');
  	//	});
    //});
});