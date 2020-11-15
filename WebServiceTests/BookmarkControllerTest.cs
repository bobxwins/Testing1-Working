using System;
using AutoMapper;
using DataServiceLib;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
 
using DataServiceLib.DBObjects;
 
using ProjectPortfolio2_Group11.Model;
using DataServiceLib.DataService;
 
using ProjectPortfolio2_Group11.Controller;
using DataServiceLib.IDataService;

using Xunit;

 
namespace WebServiceTests
{
    public class BookmarkControllerTest
    {
        private Mock<IBookmarkingDataService> _dataServiceMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IUrlHelper> _urlMock;

        public BookmarkControllerTest()
        {
             _dataServiceMock = new Mock<IBookmarkingDataService>();
             _mapperMock = new Mock<IMapper>();
             _urlMock = new Mock<IUrlHelper>();
        }


        [Fact]
        public void GetBookmartkWithValidIdSouldReturnOk()
        {

            _dataServiceMock.Setup(x => x.GetBookMark(1, "nm0000015")).Returns(new BookmarkPerson ());
           
            //{BookmarkPerson = new BookmarkPerson()}));

            
            _mapperMock.Setup(x => x.Map<BookmarkPersonDto>(It.IsAny<BookmarkPerson>())).Returns(new BookmarkPersonDto());

            

            var ctrl = new BookmarkController(_dataServiceMock.Object, _mapperMock.Object);
            ctrl.Url = _urlMock.Object;

            var response = ctrl.GetBookMark(1, "nm0000015");

            response.Should().BeOfType<OkObjectResult>(); 

        }


        [Fact]
        public void GetBookmartkWithInvalidIdSouldReturnNotFound()
        {

            var ctrl = new BookmarkController(_dataServiceMock.Object, null);

            var response = ctrl.GetBookMark(1, "nm0000015");

            response.Should().BeOfType<NotFoundResult>();

        }

    }
}
