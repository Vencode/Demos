$(document).ready(function() {
    var $tagEditor = $(".post-tag-editor");

    $tagEditor
        .find(".tag-select")
        .on("click", "> li > a", function(e) {
            e.preventDefault();

            var $this = $(this);
            var $tagParent = $this.closest("li");
            $tagParent.toggleClass("selected");

            var selected = $tagParent.hasClass("selected");
            $tagParent.find(".selected-input").val(selected);
    });

    var $addTagButton = $tagEditor.find(".add-tag-button");
    var $newTagEditor = $tagEditor.find(".new-tag-name");

    $addTagButton.click(function(e) {
        e.preventDefault();
        addTag($newTagEditor.val());
    });

    $newTagEditor
        .keyup(function() {
            if ($newTagEditor.val().trim().length > 0)
                $addTagButton.prop("disabled", false);
            else
                $addTagButton.prop("disabled", true);
        })
        .keyDown(function(e) {
        if (e.which != 13)
            return;

        e.preventDefault();
        addTag($newTagEditor.val());
    });

    function addTag(name) {
        var newIndex = $tagEditor.find(".tag-select > li").size() - 1;

        $tagEditor
            .find(".tag-select > li.template")
            .clone()
            .removeClass("template")
            .addClass("selected")
            .find(".name").text(name).end()
            .find(".name-input").val(name).attr("name", "Tags[" + newIndex + "].Name").end()
            .find(".selected-input").attr("name", "Tags[" + newIndex + "].IsChecked").val(true).end()
            .appendTo(".tag-select");

        $newTagEditor.val("");
        $addTagButton.prop("disabled", true);
    }

});