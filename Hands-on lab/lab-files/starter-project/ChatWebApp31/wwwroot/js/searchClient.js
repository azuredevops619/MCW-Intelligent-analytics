
$(document).ready(function () {
    var avatarColors = ['00A600', '55C1E7', 'F2B509', 'FF5300'];
    var uniqueUsers = [];

    $("#btnSendSearch").click(function () {
        var searchText = encodeURIComponent($("#searchText").val());
        doSearch(searchText);
    });

    $("#searchText").on("keyup", function (event) {
        if (event.keyCode == 13) {
            $("#btnSendSearch").click();
        }
    });

    function doSearch(searchText) {
      
        $.ajax({
            accepts: "application/json",
            contentType: "application/json; odata.metadata=minimal",
            headers: {
                "api-key": chatSearchApiKey
            },
            dataType: "json",
            type: "GET",
            url: chatSearchApiBase + "/indexes/" + chatSearchApiIndexName + "/docs?api-version=2019-05-06&search=" + searchText,
            success: function (data) {
                var items = [], searchResultsDiv = $("div.search-results");
                var searchResults = JSON.parse(JSON.stringify(data)).value;
                if (searchResults && searchResults.length > 0) {
                    findUniqueSearchUsers(searchResults);

                    $.each(searchResults, function (key, searchResult) {
                        items.push(createChatEntry(searchResult));
                    });

                    $("div.search-empty").hide();
                    searchResultsDiv.empty();
                    searchResultsDiv.html("<p>Found <strong>" + searchResults.length + "</strong> " + (searchResults.length === 1 ? "result" : "results") + "</p><p>&nbsp;</p>")
                    $("<ul/>", {
                        "class": "chat",
                        html: items.join("")
                    }).appendTo("div.search-results");
                }
                else {
                    searchResultsDiv.empty();
                    $("div.search-empty").show();
                }
            }
        });
    }

    function createChatEntry(searchResult) {
        var chatEntry = "", createDate, initial;
        createDate = new Date(searchResult.createDate);
        initial = searchResult.userName.substring(0, searchResult.userName.length > 1 ? 2 : 1).toUpperCase();

        chatEntry = '<li class="chatBubbleOtherUser left clearfix"><span class="chat-img pull-left">';
        //chatEntry += '<img src="https://placehold.it/50/' + getAvatarColor(searchResult.userName) + '/fff&text=' + initial + '" alt="' + searchResult.userName + '" class="img-circle" /></span>';
        chatEntry += '<div class="chat-body clearfix"><div class="header">';
        chatEntry += '<strong class="primary-font">' + searchResult.userName + '</strong><small class="pull-right search-time text-muted">';
        chatEntry += '<span class="fas fa-time"></span>&nbsp;' + createDate.toLocaleDateString() + ' ' + createDate.toLocaleTimeString() + '</small></div>';
        chatEntry += '<p>' + searchResult.message + '</p>';
        chatEntry += '</div></li>';

        return chatEntry;
    }


    function getAvatarColor(userName) {
        var idx = uniqueUsers.findIndex(function (n) { return n == userName; });
        return avatarColors[idx % 4];
    }


    function findUniqueSearchUsers(searchResults) {
        var results = searchResults.length;
        var flags = [], l = results, i;
        for (i = 0; i < l; i++) {
            if (flags[searchResults[i].userName]) continue;
            flags[searchResults[i].userName] = true;
            uniqueUsers.push(searchResults[i].userName);
        }
    }

    function addUserIfNeeded(userName) {
        var idx = uniqueUsers.findIndex(function (n) { return n == userName; });
        if (idx < 0) {
            uniqueUsers.push(userName);
        }
    }
});