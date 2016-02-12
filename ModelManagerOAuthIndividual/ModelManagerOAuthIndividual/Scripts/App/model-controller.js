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

    $scope.enterEditMode = function () {

        $http.get("/api/security/UserInRole?roleName=ModelEditorRole").success(
         function (data) {
             if (!data) {
                 // note can't use $location.path here, as the MVC Access denied page is "outside" of the 
                 // angular app, and $location.path always prepends a leading '/', so you need to use window.location.href
                 window.location.href = $location.absUrl().split('ModelEditorAngular')[0] + 'account/accessdenied';
             }

             $scope.inEditMode = true;
         });       
    }
      
    if ($routeParams.modelId.toLowerCase() === "new"){

        $scope.enterEditMode();

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

   

}]);