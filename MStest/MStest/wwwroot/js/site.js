$('.showLogin').click(function () {
    $(".signup").addClass('d-none')
    $(".login").removeClass('d-none')
    $(".forgetPassword").addClass('d-none')
    $(".verifyCode").addClass('d-none')

})
$('.tooglePassword').click(function () {
    var passInput = $(".passInput");
    if (passInput.attr('type') === 'password') {
        passInput.attr('type', 'text');
    } else {
        passInput.attr('type', 'password');
    }
})
$(".showRegister").click(function () {
    $(".login").addClass('d-none')
    $(".signup").removeClass('d-none')
})
$('.showForget').click(function () {
    $(".login").addClass('d-none')
    $(".forgetPassword").removeClass('d-none')

})
$('.showVerify').click(function () {
    $(".forgetPassword").addClass('d-none')
    $(".verifyCode").removeClass('d-none')
})
$('.nextStep').click(function () {
    $(".signup").addClass('d-none')
    $(".registerContinue").removeClass('d-none')
})
uploadPhoto.onchange = evt => {
    const [file] = uploadPhoto.files
    if (file) {
        showImage.src = URL.createObjectURL(file)
    }
}
