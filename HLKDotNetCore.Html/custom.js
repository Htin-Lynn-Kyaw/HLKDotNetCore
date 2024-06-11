function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
        (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
}

function Message(message, condition) {
    Swal.fire({
        title: condition === "error"? "Opps you got an error." : "Good job",
        text: message,
        icon: condition
        });
}

function confirmMessage() {
    let comfirmMessageResult = new Promise(function (success, error) {
        Swal.fire({
            title: "Comfirm",
            text: "Are you sure?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes"
        }).then((result) => {
            result.isConfirmed? success() : error();
        });
    });
    return comfirmMessageResult;
    // "Consuming Code" (Must wait for a fulfilled Promise)
    /*comfirmMessage.then(
        function (value) { 
            Message("Success", "success");
        },
        function (error) { 
            Message("Error", "error");
        }
    );*/
}