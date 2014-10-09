(function () {
    'use strict';

    describe('Directory', function () {
        var ctrl, $scope, $q;
        var mockDataService = {};
        var deferred;

        beforeEach(module('app'));

        beforeEach(inject(function (_$q_) {
            $q = _$q_;
        }));

        beforeEach(inject(function ($controller, $rootScope) {
            $scope = $rootScope.$new();

            ctrl = $controller('DirectoryController', {
                $scope: $scope,
                dataService: mockDataService
            });
        }));


        /*** BEGIN TESTS ***/

        it('delete employee removes employee', function () {
            $scope.employees.push({ employeeId: 1 });
            $scope.employees.push({ employeeId: 2 });
            $scope.employees.push({ employeeId: 3 });
            var deferred = $q.defer();
            var promise = deferred.promise;
            mockDataService.deleteEmployee = sinon.stub().returns(promise);

            $scope.deleteEmployee(1); //delete by index
            deferred.resolve();
            $scope.$apply();

            expect(mockDataService.deleteEmployee.calledOnce).toBe(true);
            expect($scope.employees.length).toBe(2);
        });

        it('delete employee does not remove employee when data service returns an error', inject(function () {
            $scope.employees.push({ employeeId: 1 });
            $scope.employees.push({ employeeId: 2 });
            $scope.employees.push({ employeeId: 3 });
            var deferred = $q.defer();
            var promise = deferred.promise;
            mockDataService.deleteEmployee = sinon.stub().returns(promise);

            $scope.deleteEmployee(1); //delete by index
            deferred.reject(); //fail
            $scope.$apply();

            expect(mockDataService.deleteEmployee.calledOnce).toBe(true);
            expect($scope.employees.length).toBe(3);
        }));
    });
})();
