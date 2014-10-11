(function (app) {
    'use strict';

    app.directive('visibleIf', ['Identity', function (identity) {
        return {
            restrict: 'A',
            link: link
        };

        function link($scope, $element, $attrs, $ctrl) {
            //should watch a value on identity or sub to a login event - but since logic is outside of the angular world it won't change once the app is loaded
            if (!identity.hasRole($attrs.visibleIf)) {
                $element.css('display', 'none');
            }
        }
    }]);
})(angular.module('app'));