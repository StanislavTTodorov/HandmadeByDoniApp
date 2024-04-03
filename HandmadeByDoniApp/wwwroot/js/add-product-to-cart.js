
function addProductToCart(item) {
    const id = item;

    $.ajax({
        url: '@Url.Action("Add","Order",new (id))',
        type: 'POST',
        dataType: 'json',
        data: {
            ProductId: id
        }
    })
}