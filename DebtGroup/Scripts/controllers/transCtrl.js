

app.controller('transCtrl', [
    '$scope', '$http', function($scope, $http) {

        $scope.currentDetails = [];
        $scope.detailCounter = 0;
        $scope.hasDetails = false;


        $scope.detailsData = function (itemId) {
            if (!$scope.hasDetails) {
                $http.get('/transactions/Details/' + itemId).
                    success(function (data, status, headers, config) {
                        console.log(data);
                        $scope.currentDetails.push(data);
                        $scope.newDetails = $scope.currentDetails;
                        $scope.storeFirst = data.FirstName;
                        $scope.storeLast = data.LastName;
                        $scope.storeAmount = data.Amount;
                        $scope.storeDescr = data.Description;
                        $scope.storeSplit = data.SplitWith;
                        console.log(data.FirstName);
                    }).
                    error(function(data, status, headers, config) {
                    });
                $scope.newDetails = '';
                $scope.hasDetails = true;
            }
        }

        $scope.editDetails = function() {
            $('.editInput').attr('readonly', false);
            $('.editButton').addClass('hide');
            $('.cancelButton').removeClass('hide');
            $('.saveChanges').removeClass('hide');
            
        }

        $scope.saveChanges = function() {
            $('.editInput').attr('readonly', true);
            $('.editButton').removeClass('hide');
            $('.cancelButton').addClass('hide');
            $('.saveChanges').addClass('hide');
        }

        $scope.cancelChanges = function () {
            //Back to normal layout
            $('.editInput').attr('readonly', true);
            $('.editButton').removeClass('hide');
            $('.cancelButton').addClass('hide');
            $('.saveChanges').addClass('hide');

            //Revert Changes
            $('.dataPurchaser').val($scope.storeFirst + " " + $scope.storeLast);
            $('.dataAmount').val($scope.storeAmount);
            $('.dataDescr').val($scope.storeDescr);
            $('.dataSplit').val($scope.storeSplit);
        }



    }
]);