using Core.Entities;
using Mapster;
using WebApi.Models.Dish;
using WebApi.Models.User;

namespace WebApi.Mapsters
{
    public class MapsterConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, UserDto>();
            config.NewConfig<UserEditModel, User>();

            config.NewConfig<Dish, DishDto>();
            config.NewConfig<DishEditModel, User>();

            //config.NewConfig<Menu, MenuDto>();
            //config.NewConfig<MenuEditModel, Menu>();

            //config.NewConfig<Menu, MenuDto>()
            //    .Map(dest => dest.FoodCount,
            //        src => src.Foods == null ? 0 : src.Foods.Count);
            //config.NewConfig<MenuEditModel, Menu>();

            //config.NewConfig<Contact, ContactDto>();
            //config.NewConfig<ContactEditModel, Contact>();

            //config.NewConfig<Food, FoodDto>();
            //config.NewConfig<IList<Food>, IList<FoodDto>>();
            //config.NewConfig<FoodEditModel, Food>();







            //// Comment
            //config.NewConfig<Comment, CommentDto>();

            //config.NewConfig<CommentEditModel, Comment>();


            //// Post
            //config.NewConfig<Post, PostDto>();
            //config.NewConfig<Post, PostDetail>();

            //config.NewConfig<PostFilterModel, PostQuery>()
            //    .Map(dest => dest.PublishedOnly, src => false);

            //config.NewConfig<IList<Post>, IList<PostDto>>();
        }
    }
}
