$(document).ready(function () {
    // Sample data for state and city options (replace this with your actual data)
    var statesData = {
        "India": ["Delhi", "Mumbai", "Chennai"],
        "Iran": ["Tehran", "Shiraz", "Isfahan"],
        "Thailand": ["Bangkok", "Phuket", "Chiang Mai"],
        "Pakistan": ["Islamabad", "Karachi", "Lahore"],
        "Dubai": ["Dubai City", "Abu Dhabi", "Sharjah"]
    };

    // Function to populate the "State" dropdown based on the selected "Country"
    function populateStates() {
        var selectedCountry = $("#countrySelect").val();
        var states = statesData[selectedCountry];
        var stateSelect = $("#stateSelect");
        stateSelect.empty();
        $.each(states, function (index, state) {
            stateSelect.append($('<option></option>').val(state).text(state));
        });
    }

    // Function to populate the "City" dropdown based on the selected "State"
    function populateCities() {
        var selectedState = $("#stateSelect").val();
        // Sample data for city options (replace this with your actual data)
        var citiesData = {
            "Delhi": ["New Delhi", "Old Delhi"],
            "Mumbai": ["Mumbai City", "Navi Mumbai"],
            "Chennai": ["Chennai City", "Chennai Suburb"],
            "Tehran": ["Tehran City", "Tehran Suburb"],
            // Add city data for other states here
        };
        var cities = citiesData[selectedState];
        var citySelect = $("#citySelect");
        citySelect.empty();
        $.each(cities, function (index, city) {
            citySelect.append($('<option></option>').val(city).text(city));
        });
    }

    // Event listeners to trigger dynamic population of dropdowns
    $("#countrySelect").change(function () {
        populateStates();
        populateCities(); // Clear the city dropdown when country changes
    });

    $("#stateSelect").change(function () {
        populateCities();
    });

    // Initial population of "State" dropdown based on the default selected "Country"
    populateStates();
    // Initial population of "City" dropdown based on the default selected "State"
    populateCities();
});