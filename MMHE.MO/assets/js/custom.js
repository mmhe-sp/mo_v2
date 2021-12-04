function getJSLStatusBackground()
{
    var statuses = $('.jsl-status');
    $.each(statuses, function (i, v)
    {
        var s = $(v);
        var cssClass = '';
        switch (s.text().toLocaleLowerCase())
        {
            case 'client approved':
                cssClass = 'badge-soft-approved';
                break;
            case 'pending':
                cssClass = 'badge-soft-warning';
                break;
            case 'p/s approved':
                cssClass = 'badge-soft-success';
                break;
            case 'cancelled':
                cssClass = 'badge-danger-success';
                break;
        }
        s.addClass(cssClass);
    });
}