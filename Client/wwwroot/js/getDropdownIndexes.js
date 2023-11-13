function getDropdownIndexes() {
    const elements = document.querySelectorAll('[id^=comment-menu-]');
    const indexes = [];
    if (elements.length > 0) {
        for (let i = 0; i < elements.length; i++) {
            const element = elements[i];
            indexes.push(parseInt(element.id.split("comment-menu-").pop()));
        }
    }
    
    return indexes;
}
