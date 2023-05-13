var imageNumber = 0;

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
$('.toogleConfirmPassword').click(function () {
    var passInput = $(".passConfirmInput");
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
$('.newPasswordbtn').click(function () {
    $(".verifyCode").addClass('d-none')
    $(".newPassword").removeClass('d-none')
})
// $('.nextStep').click(function(){
//     $(".signup").addClass('d-none')
//     $(".registerContinue").removeClass('d-none')
// })

//Edit Image Profile
//uploadPhoto.onchange = evt => {
//    const [file] = uploadPhoto.files
//    if (file) {
//        showImage.src = URL.createObjectURL(file)
//    }
//  }
//Upload Image Notes
function loadFile(event) {
    console.log(event)
    console.log(event.srcElement.parentElement.children[1].children[0])
    var reader = new FileReader();
    reader.onload = function () {
        var output = event.srcElement.parentElement.children[1].children[0];
        output.src = reader.result;
    };
    reader.readAsDataURL(event.target.files[0]);
};

function appendNotes(ele) {
    ++imageNumber
    console.log(imageNumber)
    console.log(typeof imageNumber)
    $('#appendNotes').before(
        `<div class="col-md-4 mt-4">
            <form class="card border-0 shadow">
                <input type="file" id="addImage_${imageNumber}" class="d-none" onchange="loadFile(event)"/>
                <label for="addImage_${imageNumber}">
                    <img src="~/img/addimage.jpg" class="img-fluid appendImage" id="appendImage_${imageNumber}" alt="">
                </label>
                <input type="text" class="form-control my-4 w-75 mx-auto" placeholder="Enter your Note"/>
                <button class="btn btn-load">Submit</button>
            </form>
        </div>`)
}
var i = 0;
function addNewVlaue(event) {
    i++
    $("#row").after(`<div class="row" id="row_${i}">
                        <div class="col-5">
                            <label for="">Medicine Name</label>
                            <input type="text" class="form-control">
                        </div>
                        <div class="col-5">
                            <label for="">Times per day</label>
                            <input type="text" class="form-control">
                        </div>
                        <div class="col-2">
                            <label for=""></label>
                            <button class="btn btn-plus" data-id="${i}" onclick="deletewVlaue(event)" type="button"> - </button>
        </div>
    </div>

                        `);
}
function deletewVlaue(ele) {
    $(`#row_${ele.srcElement.dataset.id}`).remove();
}
function hidefirstform() {
    $('#first_form').addClass('d-none')
    $('#sec_form').removeClass('d-none')
}
function showfirstform() {
    $('#sec_form').addClass('d-none')
    $('#first_form').removeClass('d-none')

}
var swiper = new Swiper(".mySwiper", {
    effect: "coverflow",
    grabCursor: true,
    centeredSlides: true,
    slidesPerView: "auto",
    // slidesPerView: 3,

    coverflowEffect: {
        rotate: 50,
        stretch: 0,
        depth: 100,
        modifier: 1,
        slideShadows: true,
    },
    centeredSlides: true,
    centeredSlides: true,
    autoplay: {
        delay: 2500,
        disableOnInteraction: false,
    },
    pagination: {
        el: ".swiper-pagination",
        clickable: true,
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    }, navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
});

jQuery(document).ready(function () {

    $(".chat-list a").click(function () {
        $(".chatbox").addClass('showbox');
        return false;
    });

    $(".chat-icon").click(function () {
        $(".chatbox").removeClass('showbox');
    });


});