const tblBlog = "blogs";
let blogID = null;
GetBlogTabel();
//CreateBlog();

function Readblog() {
    console.log(GetBlogs());
}

function EditBlog(id) {
    let lst = GetBlogs();

    const item = lst.filter(x => x.id === id);

    if (item.length == 0) {
        console.log("no data found");
        Message("no data found");
        return;
    }
    //return item[0];
    bindControls(item[0]);
}

function CreateBlog(title, author, content) {
    let lst = GetBlogs();

    const reqModel = {
        id: uuidv4(),
        title: title,
        author: author,
        content: content
    };

    lst.push(reqModel);
    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);

    clearControls();

    Message("Saving successful.");
}

function UpdateBlog(id, title, author, content) {
    let lst = GetBlogs();

    const item = lst.filter(x => x.id === id);

    if (item.length == 0) {
        console.log("no data found");
        return;
    }

    const anItem = item[0];
    anItem.title = title;
    anItem.author = author;
    anItem.content = content;

    const index = lst.findIndex(x => x.id === id);
    lst[index] = anItem;

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);

    clearControls();

    Message("Update successful.");
}

function DeleteBlog(id) {
    let result = confirm("Are you sure?");
    if(!result) return;

    let lst = GetBlogs();

    const item = lst.filter(x => x.id === id);

    if (item.length == 0) {
        console.log("no data found");
        return;
    }

    lst = lst.filter(x => x.id !== id);

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);

    Message("Delete successful");

    GetBlogTabel();
}

function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
        (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
}

function GetBlogs() {
    const blogs = localStorage.getItem(tblBlog);

    let lst = [];
    if (blogs !== null) {
        lst = JSON.parse(blogs);
    }
    return lst;
}

function Message(message) {
    alert(message);
}

function clearControls() {
    $('#txtTitle').val('');
    $('#txtAuthor').val('');
    $('#txtContent').val('');
    $('#txtTitle').focus();
}

function bindControls(item) {
    blogID = item.id;
    $('#txtTitle').val(item.title);
    $('#txtAuthor').val(item.author);
    $('#txtContent').val(item.content);
    $('#txtTitle').focus();
}

$('#btnSave').click(function () {
    const title = $('#txtTitle').val();
    const author = $('#txtAuthor').val();
    const content = $('#txtContent').val();

    if (blogID === null) {
        CreateBlog(title, author, content);
    } else {
        UpdateBlog(blogID, title, author, content);
        blogID = null;
    }  

    GetBlogTabel();
})

function GetBlogTabel() {
    const lst = GetBlogs();
    let count = 0;
    let htmlRows = '';

    lst.forEach(x => {
        const htmlRow = ` 
        <tr>
            <td>${++count}</td>
            <td>${x.title}</td>
            <td>${x.author}</td>
            <td>${x.content}</td>
            <td>
                <button type="button" class="btn btn-primary" onclick="EditBlog('${x.id}')">Edit</button> |
                <button type="button" class="btn btn-danger" onclick="DeleteBlog('${x.id}')">Delete</button>
            </td>
        </tr>
        `;

        htmlRows += htmlRow;
    });

    $("#tbody").html(htmlRows);
}
