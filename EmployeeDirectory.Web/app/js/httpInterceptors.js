(function (app) {
    'use strict';

    //http://blog.thesparktree.com/post/75952317665/angularjs-interceptors-globally-handle-401-and-other
    app.factory('authHttpResponseInterceptor', ['$q', '$location', '$window', function ($q, $location, $window) {
        return {
            response: function (response) {
                return response;
            },
            responseError: function (rejection) {
                if (rejection.status === 401) {
                    $window.location.href = '/Account/Login?returnUrl=' + $window.encodeURIComponent($location.path());
                }
                return $q.reject(rejection);
            }
        }
    }]);
})(angular.module('app'));