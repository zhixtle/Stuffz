var GetSearchResultsUrl = '/Home/GetSearchResults/';
var SearchResultsUrl = '/Home/SearchResults/';


$(document).ready(function () {
    $('#btnSearch').on('click', function () {

        var searchData = {
            searchQuery: $('#txtSearchQuery').val()
        }

        $.ajax({
            url: GetSearchResultsUrl,
            data: searchData,
            success: function (data) {
                SearchSuccess(data)
            },
            error: function () {
                alert('NOOOOOOOOOOOOOO');
            }
        });

        function SearchSuccess(data) {
            $('#searchResults').load(SearchResultsUrl, { searchQuery: data.results });
        }
    });
});