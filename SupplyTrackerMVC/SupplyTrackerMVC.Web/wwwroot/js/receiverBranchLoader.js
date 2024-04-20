document.getElementById('SelectedReceiverId').addEventListener('change', function () {
    var receiverId = this.value;
    fetch(`/Delivery/GetReceiverBranches?receiverId=${receiverId}`)
        .then(response => response.json())
        .then(data => {

            // Remember to remove console.log
            console.log("Received Branches Data => ", data);

            var branchesSelect = document.getElementById('SelectedReceiverBranchId');
            branchesSelect.innerHTML = '';
            data.receiverBranches.forEach(branch => {
                var option = new Option(branch.branchAlias, branch.id);
                branchesSelect.appendChild(option);
            });
        })
        .catch(error => console.error('Error loading the branches:', error));
});