var app = angular.module('ModelManagerApp', ['ngRoute']);

app.config(['$routeProvider', '$locationProvider',
        function ($routeProvider, $locationProvider) {
            $routeProvider.when('/', {
                templateUrl: 'partials/model-list.html',
                controller: 'ModelListCtrl'
            }).
            when('/:modelId', {
                templateUrl: 'partials/model-detail.html',
                controller: 'ModelDetailCtrl'
            }).
            otherwise({
                redirectTo: '/'
            })
        }

]);


app.controller('ModelListCtrl', function ($scope, $http) {

    $http.get("/api/models").success(
            function (data) {
                $scope.models = data;
            });
});

app.controller('ModelDetailCtrl', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {

    $http.get("/api/status").success(
        function (data) {
            $scope.statusList = data;
        });

    $http.get("/api/manufacturers").success(
        function (data) {
            $scope.manufacturers = data;
        });

    $http.get("/api/categories").success(
        function (data) {
            $scope.categories = data;
        });
      
    if ($routeParams.modelId.toLowerCase() === "new"){
        $scope.inEditMode = true;

        $scope.model = {
            ModelId: null,
            ManufacturerName: '',
            ManufacturerId: null,
            CategoryName: '',
            CategoryId: null,
            ManufacturerCode: '',
            ModelName: '',
            StatusName: '',
            StatusId: null,
            ListPrice: 0.00,
            Description:''
        }
    }
    else {
        $scope.inEditMode = false;

        $http.get("/api/models/" + $routeParams.modelId).success(
            function (data) {
                $scope.model = data;

            });

    }

    $scope.saveModel = function () {

        console.log("saving");

        if ($scope.model.ModelId > 0) {
            console.log("saving - PUT");
            $http.put("/api/models/" + $scope.model.ModelId, $scope.model).success(function (data, status) {
                $scope.inEditMode = false;
            });
        }
        else {
            console.log("saving - POST");
            $http.post("/api/models/", $scope.model).success(function (data, status) {
                $scope.inEditMode = false;
                $location.path('/');
            });
        }

    }

    $scope.enterEditMode = function () {
        if ($scope.inEditMode === true)
            $scope.inEditMode = false;
        else {
            $scope.inEditMode = true;
        }
    }

}]);