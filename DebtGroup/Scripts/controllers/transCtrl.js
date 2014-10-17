

app.controller('transCtrl', [
    '$scope', '$http', function($scope, $http) {

        $scope.currentDetails = [];
        $scope.detailCounter = 0;
        $scope.hasDetails = false;
        $scope.detailsData = function (itemId) {
            if (!$scope.hasDetails) {
                $http.get('/transactions/Details/' + itemId).
                    success(function(data, status, headers, config) {
                        $scope.currentDetails.push(data);
                        $scope.newDetails = $scope.currentDetails;
                    }).
                    error(function(data, status, headers, config) {
                    });
                $scope.newDetails = '';
                $scope.hasDetails = true;
            }
        }



    }
]);