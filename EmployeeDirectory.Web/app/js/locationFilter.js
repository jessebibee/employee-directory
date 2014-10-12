(function (app) {
    'use strict';

    app.filter('location', ['$filter', function ($filter) {
        var locations = {
            1: 'Austin',
            2: 'Dallas',
            3: 'Houston'
        };

        return function (input) {
            return locations[input] || '';
        };
    }]);
})(angular.module('app'));