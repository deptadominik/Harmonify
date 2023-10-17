function searchForFriends() {
    $('.ui.search')
        .search({
            apiSettings: {
                url: '/ApplicationUser/by-phrase?phrase={query}'
            },
            fields: {
                results : 'items',
                title   : 'fullName',
                description: 'description',
                image: 'avatarSource',
                url     : 'profileUrl'
            },
        });
}