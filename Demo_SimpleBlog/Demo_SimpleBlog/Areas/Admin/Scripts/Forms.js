$(Document).ready(function() {
    $("a[data-post]").click(function(e) {
        e.preventDefault();

        var $this = $(this);
        var message = $this.data("post");

        if (message && !confirm(message))
            return;

        var antiForgeryToken = $("#anti-forgery-form input");
        var antiForgeryInput = $("<input type='hidden'>").attr("name", antiForgeryToken.attr("name")).val(antiForgeryToken.val());

        $("<form>")
            .attr("method", "post")
            .attr("action", $this.attr("href"))
            .append(antiForgeryInput)
            .appendTo(document.body)
            .submit();
    });

    $("[data-slug]").each(function() {
        var $this = $(this);
        var $sendSlugFrom = $($this.data("slug"));

        $sendSlugFrom.keyup(function() {
            var slug = $sendSlugFrom.val();

            slug = slug.replace(/[^a-zA-z0-9\s]/g, "");  //Removes every special character 
            slug.toLowerCase();
            slug = slug.replace(/\s+/g, "-"); //Replaces every space with a -

            if (slug.charAt(slug.length - 1) == "-")
                slug = slug.substr(0, slug.length - 1); //Trim final character if it is an -

            $this.val(slug);
        });
    });
});