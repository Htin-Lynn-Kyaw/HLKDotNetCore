$(document).ready(function() {
    var cart = {};

    function updateCart() {
        var totalPrice = 0;
        var $cartTableBody = $('#cartTable tbody');
        $cartTableBody.empty();
        
        $.each(cart, function(productName, product) {
            var row = `<tr>
                <td>${productName}</td>
                <td>${product.quantity}</td>
                <td>$${(product.price * product.quantity).toFixed(2)}</td>
                <td><button class="btn btn-danger remove-from-cart"> - </button></td>
            </tr>`;
            $cartTableBody.append(row);
            totalPrice += product.price * product.quantity;
        });

        $('#totalPrice').text(totalPrice.toFixed(2));
    }

    $('.add-to-cart').click(function() {
        var $row = $(this).closest('tr');
        var productName = $row.find('td:nth-child(2)').text();
        var price = parseFloat($row.find('td:nth-child(3)').text().replace('$', ''));

        if (cart[productName]) {
            cart[productName].quantity += 1;
        } else {
            cart[productName] = { price: price, quantity: 1 };
        }

        updateCart();
    });

    $('#cartTable').on('click', '.remove-from-cart', function() {
        var $row = $(this).closest('tr');
        var productName = $row.find('td:nth-child(1)').text();

        if (cart[productName].quantity > 1) {
            cart[productName].quantity -= 1;
        } else {
            delete cart[productName];
        }

        updateCart();
    });
});