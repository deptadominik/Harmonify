function errorToast(message) {
    $.toast({
        title: 'Error',
        position: 'bottom right',
        class: 'red',
        className: {
            toast: 'ui message'
        },
        message: message,
        showProgress: 'bottom'
    })
}