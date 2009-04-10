<%@ Control Language="C#" Inherits="Suteki.Shop.ViewUserControl<Category>" %>
<div onclick="location.href='<%= Url.Action<ProductController>(c=>c.Index(Model.CategoryId)) %>'" class="product">
    <div><%= Model.Name %></div>

    <% if(Model.HasMainImage) { %>
        <%= Html.Image("~/ProductPhotos/" + Model.MainImage.ThumbFileName) %>
    <% } %>
</div>
