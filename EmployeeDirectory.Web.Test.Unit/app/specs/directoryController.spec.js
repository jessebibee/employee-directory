(function () {
    'use strict';

    describe('Directory', function () {
        var ctrl, $scope;

        beforeEach(function () {
            module('app');

            inject(function ($controller, $rootScope) {
                $scope = $rootScope.$new();

                ctrl = $controller('DirectoryController', {
                    $scope: $scope
                });
            });
        });

        it('should load with 0 employees', inject(function () {
            expect($scope.employees.length).toEqual(0);
        }));
    });
})();
