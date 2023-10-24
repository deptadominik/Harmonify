function successToast(message) {
    $.toast({
        title: 'Success',
        position: 'bottom right',
        class: 'success',
        className: {
            toast: 'ui positive message'
        },
        message: message,
        showProgress: 'bottom'
    })
}