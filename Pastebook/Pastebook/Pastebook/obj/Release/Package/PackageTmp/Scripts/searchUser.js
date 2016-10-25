var GetSearchResultsUrl = '/Home/GetSearchResults/';
var SearchResultsUrl = '/Home/SearchResults/';


$(document).ready(function () {
    $('#btnSearch').on('click', function () {

        var query = $('#txtSearchQuery').val();
        query = query.replace(/</g, '&lt;').replace(/>/g, '&gt;');

        var searchData = {
            searchQuery: query
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