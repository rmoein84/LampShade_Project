const cookieName = "cart-items";
function setParentId(parentId) {
    $('#ParentId').val(parentId);
}

function addToCart(id, title, price, picture) {
    let products = $.cookie(cookieName);
    if (products === undefined) {
        products = [];
    } else {
        products = JSON.parse(products);
    }
    const count = $("#productCount").val();
    const currentProduct = products.find(x => x.id === id);
    if (currentProduct !== undefined) {
        products.find(x => x.id === id).count = parseInt(currentProduct.count) + parseInt(count);
    } else {
        const product = {
            id,
            title,
            unitPrice: price,
            picture,
            count
        }
        products.push(product);
    }
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" })
    updateCart();
}
function updateCart() {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    $("#cart_count").text(products.length)
    let cartItemsWrapper = $("#cart_items_wrapper");
    cartItemsWrapper.html('');
    products.forEach(p => {
        const product = `<div class="single-cart-item">
                            <a onClick="removeFromCart('${p.id}')" class="remove-icon">
                            <i class="ion-android-close"></i>
                            </a>
                            <div class="image">
                                <a href="single-product.html">
                                    <img src="/uploadedFiles/${p.picture}"
                                    class="img-fluid">
                                </a>
                            </div>
                            <div class="content">
                                <p class="product-title">
                                    <a href="single-product.html">محصول: ${p.title}</a>
                                </p>
                                <p class="count">تعداد: ${p.count}</p>
                                <p class="count">قیمت واحد: ${p.unitPrice}</p>
                            </div>
                        </div>`;
        cartItemsWrapper.append(product);

    });
}
function removeFromCart(id) {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    let itemToRemove = products.find(x => x.id === id);
    products.splice(products.indexOf(itemToRemove), 1);
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" })
    updateCart();
}
function changeCartItemCount(id, count) {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    let product = products.find(x => x.id === id);
    let totalPrice = count * product.unitPrice
    product.count = count;
    $(`#totlaPrice-${id}`).text(formatter.format(totalPrice));
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" })
    updateCart();

    var settings = {
        "url": "https://localhost:7127/api/inventory/",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "productId": id,
            "count": count
        }),
    };

    $.ajax(settings).done(function (data) {
        if (data.isStock == false) {
            const warningsDiv = $('#productStockWarnings');
            if ($(`#${id}`).length == 0) {
                warningsDiv.append(`
                    <div class="alert alert-warning" id="${id}">
                        <i class="fa fa-warning"></i> کالای
                        <strong>${data.productName}</strong>
                        در انبار کمتر از تعداد درخواستی موجود است.
                    </div>
                `);
            }
        }
        else {
            if ($(`#${id}`).length > 0) {
                $(`#${id}`).remove();
            }
        }
    });
}


const formatter = new Intl.NumberFormat();