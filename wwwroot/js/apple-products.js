// Apple Products JavaScript

$(document).ready(function() {
    // Image gallery functionality for product details
    $('.thumbnail-img').click(function() {
        $('.thumbnail-img').removeClass('active');
        $(this).addClass('active');
        
        // Get the product slug from the clicked thumbnail
        var productSlug = $(this).data('product');
        
        // Update main image
        $('#mainImage').attr('data-product', productSlug);
        $('#mainImage').removeClass().addClass('product-image').attr('data-product', productSlug);
    });
    
    // Smooth scrolling for anchor links
    $('a[href^="#"]').on('click', function(event) {
        var target = $(this.getAttribute('href'));
        if( target.length ) {
            event.preventDefault();
            $('html, body').stop().animate({
                scrollTop: target.offset().top - 100
            }, 1000);
        }
    });
    
    // Product card hover effects
    $('.product-card').hover(
        function() {
            $(this).find('.product-image').css('transform', 'scale(1.05)');
        },
        function() {
            $(this).find('.product-image').css('transform', 'scale(1)');
        }
    );
    
    // Category card click handlers
    $('.category-card').click(function() {
        var categoryName = $(this).find('.card-title').text().toLowerCase();
        var categoryId = getCategoryId(categoryName);
        if (categoryId) {
            window.location.href = '/Products?categoryId=' + categoryId;
        }
    });
});

// Helper function to get category ID
function getCategoryId(categoryName) {
    const categoryMap = {
        'iphone': 1,
        'macbook': 2,
        'ipad': 3,
        'apple watch': 4,
        'airpods': 5
    };
    return categoryMap[categoryName];
}

// Add loading animation for images
function addImageLoadingEffect() {
    $('.product-image').each(function() {
        $(this).addClass('loading');
        
        // Simulate image loading
        setTimeout(() => {
            $(this).removeClass('loading').addClass('loaded');
        }, Math.random() * 1000 + 500);
    });
}

// Initialize loading effects
$(window).on('load', function() {
    addImageLoadingEffect();
});